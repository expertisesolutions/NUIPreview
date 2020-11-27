/*
 * Copyright (c) 2016 Samsung Electronics Co., Ltd All Rights Reserved
 *
 * Licensed under the Apache License, Version 2.0 (the License);
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an AS IS BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ElmSharp.Wearable
{
    class RotarySelectorList : IList<RotarySelectorItem>
    {
        RotarySelector _owner;
        List<RotarySelectorItem> Items { get; set; }

        public RotarySelectorList(RotarySelector owner)
        {
            this._owner = owner;
            Items = new List<RotarySelectorItem>();
        }

        public RotarySelectorItem this[int index] { get => Items[index]; set => Items[index] = value; }

        public int Count => Items.Count;

        public bool IsReadOnly => false;

        public void Add(RotarySelectorItem item)
        {
            item.Handle = Interop.Eext.eext_rotary_selector_item_append(_owner);
            Items.Add(item);
        }

        public void Clear()
        {
            Interop.Eext.eext_rotary_selector_items_clear(_owner);
        }

        public bool Contains(RotarySelectorItem item)
        {
            return Items.Contains(item);
        }

        public void CopyTo(RotarySelectorItem[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        public IEnumerator<RotarySelectorItem> GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int IndexOf(RotarySelectorItem item)
        {
            return Items.IndexOf(item);
        }

        public void Insert(int index, RotarySelectorItem item)
        {
            if (Items.Count <= index || index < 0)
            {
                throw new ArgumentOutOfRangeException("index is not valid in the RotarySelector");
            }
            RotarySelectorItem target = Items[index];
            item.Handle = Interop.Eext.eext_rotary_selector_item_insert_after(_owner, target.Handle);
            Items.Insert(index, item);
        }

        public bool Remove(RotarySelectorItem item)
        {
            if (Items.Contains(item))
            {
                Interop.Eext.eext_rotary_selector_item_del(item.Handle);
                Items.Remove(item);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (Items.Count < index + 1 || index < 0)
            {
                throw new ArgumentOutOfRangeException("index is not valid in the RotarySelector");
            }

            RotarySelectorItem target = Items[index];
            Interop.Eext.eext_rotary_selector_item_del(target.Handle);
            target.Handle = IntPtr.Zero;
            Items.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }
    }
}
