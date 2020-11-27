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
using ElmSharp;

namespace ElmSharp.Test.Wearable
{
    class GenListTest4 : WearableTestCase
    {
        public override string TestName => "GenListTest4";
        public override string TestDescription => "To test Append/Prepend/InsertBefore operation of GenList";

        public override void Run(Window window)
        {
            Conformant conformant = new Conformant(window);
            conformant.Show();
            GenList list = new GenList(window)
            {
                Homogeneous = true,
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1
            };

            GenItemClass defaultClass = new GenItemClass("default")
            {
                GetTextHandler = (obj, part) =>
                {
                    return string.Format("{0} - {1}", (string)obj, part);
                }
            };

            GenListItem[] items = new GenListItem[100];
            for (int i = 0; i < 100; i++)
            {
                if (i < 30)
                {
                    items[i] = list.Append(defaultClass, string.Format("{0} Item", i));
                }
                else if (i < 60)
                {
                    items[i] = list.Prepend(defaultClass, string.Format("{0} Item", i));
                }
                else
                {
                    items[i] = list.InsertBefore(defaultClass, string.Format("{0} Item", i), items[50]);
                }
            }
            list.Show();
            list.ItemSelected += List_ItemSelected; ;
            conformant.SetContent(list);
        }

        private void List_ItemSelected(object sender, GenListItemEventArgs e)
        {
            Console.WriteLine("{0} Item was selected", (string)(e.Item.Data));
        }
    }
}
