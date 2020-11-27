#!/bin/bash

TARGET_USER=owner
TARGET_DIR=/home/$TARGET_USER/elmsharp_test
TARGET_RES_DIR=$TARGET_DIR/res

TO_COPY_FILES="bin/Debug"

exit_on_error() {
  if [ $1 -ne "0" ]
  then
    exit $1
  fi
}

sdb_cmd() {
  sdb shell su -l $TARGET_USER -c "$1"
}

usage() {
  echo "Usage: $0 [options] [testcase]"
  echo "    Options:"
  echo "        -h, --help         Show this usages message"
  echo "        -i, --install      Install test cases to target device"
  echo "        -r, --install-res  Install resource files for test cases to target device"
  echo "        -m, --use-mono     Execute test case by using mono instead of corerun"
}

install() {
  echo "install"
  sdb root on
  sdb_cmd "rm -fr $TARGET_DIR"
  sdb_cmd "mkdir -p $TARGET_DIR"
  sdb push $TO_COPY_FILES/Tizen*.dll $TARGET_DIR
  sdb push $TO_COPY_FILES/ElmSharp*.dll $TARGET_DIR
  sdb push $TO_COPY_FILES/ElmSharp*.exe $TARGET_DIR
  exit_on_error $?
}

install_res() {
  sdb root on
  sdb_cmd "rm -fr $TARGET_RES_DIR"
  sdb_cmd "mkdir -p $TARGET_RES_DIR"
  sdb push $TO_COPY_FILES/res $TARGET_RES_DIR
  exit_on_error $?
}

run() {
  sdb root on
  sdb_cmd "/usr/share/tizen.net/corerun $TARGET_DIR/ElmSharp.Test.exe $1"
  exit_on_error $?
}

run_mono() {
  sdb root on
  sdb_cmd "MONO_TRACE_LISTENER=Console.Error mono $TARGET_DIR/ElmSharp.Test.exe $1"
  exit_on_error $?
}

OPTS=`getopt -o hirm --long help,install,install-res,use-mono -n 'test' -- "$@"`
if [ $? != 0 ] ; then echo "Failed parsing options." >&2 ; usage; exit 1 ; fi
eval set -- "$OPTS"

FLAG_HELP=false
FLAG_INSTALL=false
FLAG_INSTALL_RES=false
FLAG_USE_MONO=false

while true; do
  case "$1" in
    -h|--help) FLAG_HELP=true; shift ;;
    -i|--install) FLAG_INSTALL=true; shift ;;
    -r|--install-res) FLAG_INSTALL_RES=true; shift ;;
    -m|--use-mono) FLAG_USE_MONO=true; shift ;;
    --) shift; break ;;
    *) break ;;
  esac
done

if $FLAG_HELP; then usage; exit 1; fi

if $FLAG_INSTALL; then install; fi
if $FLAG_INSTALL_RES; then install_res; fi
if $FLAG_USE_MONO; then run_mono $@; else run $@; fi
