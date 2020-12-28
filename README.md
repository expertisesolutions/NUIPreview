NUIPreview
==========

NUIPreview is a [Visual Studio](https://visualstudio.microsoft.com/) plugin to preview
[Tizen.NUI](https://docs.tizen.org/application/dotnet/guides/nui/overview/)
user interfaces designed through
[XAML](https://docs.tizen.org/application/dotnet/guides/nui/xaml/xaml-support-for-tizen-nui/)
files.

Building dependencies
=====================

The binary dependencies for the plugin are stored in [NUIPreview/deps](NUIPreview/deps)
and the build system bundles them inside the plugin binary. Therefore normal users don't
need to build the dependencies, but you may need to update a dependency. As Visual Studio
is a 32 bits process, you need to build all dependencies as 32 bits as well. First of all
clone the `VSIX` fork of [DALi](https://docs.tizen.org/application/native/guides/ui/dali/):

```sh
$ git clone -b vsix git://github.com/expertisesolutions/dali-core
$ git clone -b vsix git://github.com/expertisesolutions/dali-adaptor
$ git clone -b vsix git://github.com/expertisesolutions/dali-toolkit
$ git clone -b vsix git://github.com/expertisesolutions/dali-csharp-binder
$ git clone -b vsix git://github.com/expertisesolutions/tizenfx-stub
```

Our fork has nothing special, it is just DALi maste with a few patches that might
not be merged yet. Also you need the
[windows-dependencies](https://github.com/dalihub/window-dependencies) repo.

To build, make sure you followed the
[README file](https://github.com/dalihub/dali-core/blob/master/README.md)
of dali-core to setup [vcpkg](https://github.com/Microsoft/vcpkg).

Save the following bat file inside the directory where you cloned the repositories.
Change the `base_dir` to your work directory.

```bat
set base_dir=C:\work
set common_args=-g "Visual Studio 16 2019" -A Win32 -DCMAKE_INSTALL_PREFIX=%base_dir%\dali-env \
-DCMAKE_TOOLCHAIN_FILE=%base_dir%\vcpkg\scripts\buildsystems\vcpkg.cmake -DCMAKE_BUILD_TYPE=Debug -DENABLE_DEBUG=ON -Wno-dev

REM window-dependencies
cd %base_dir%\windows-dependencies\build\
rd /Q /S build
mkdir build
cd build
cmake %common_args% .. || goto :error
cmake --build . --target install --config debug || goto :error

REM dali-core
cd %base_dir%\dali-core\build\tizen
rd /Q /S build-windows
mkdir build-windows
cd build-windows
cmake %common_args% -DENABLE_PKG_CONFIGURE=OFF -DENABLE_LINK_TEST=OFF -DINSTALL_CMAKE_MODULES=ON .. || goto :error
cmake --build . --target install --config debug || goto :error

REM dali-adaptor
cd %base_dir%\dali-adaptor\build\tizen
rd /Q /S build-windows
mkdir build-windows
cd build-windows
cmake %common_args% -DENABLE_PKG_CONFIGURE=OFF -DENABLE_LINK_TEST=OFF -DINSTALL_CMAKE_MODULES=ON -DPROFILE_LCASE=windows .. || goto :error
cmake --build . --target install --config debug || goto :error

REM dali-toolkit
cd %base_dir%\dali-toolkit\build\tizen
rd /Q /S build-windows
mkdir build-windows
cd build-windows
cmake %common_args% -DENABLE_PKG_CONFIGURE=OFF -DENABLE_LINK_TEST=OFF -DINSTALL_CMAKE_MODULES=ON -DUSE_DEFAULT_RESOURCE_DIR=OFF .. || goto :error
cmake --build . --target install -j1 --config debug || goto :error

REM dali-csharp-binder
cd %base_dir%\dali-csharp-binder\build\tizen
rd /Q /S build-windows
mkdir build-windows
cd build-windows
cmake %common_args% .. || goto :error
cmake --build . --target install --verbose --config debug || goto :error

tizenfx-stub
cd %base_dir%\tizenfx-stub
rd /Q /S build-windows
mkdir build-windows
cd build-windows
cmake %common_args% .. || goto :error
cmake --build . --target install --verbose --config debug || goto :error

:error
cd %base_dir%
```

Run the bat file to build all.

Plugin demo
==========

https://www.youtube.com/embed/67ccJSP7Vi8
