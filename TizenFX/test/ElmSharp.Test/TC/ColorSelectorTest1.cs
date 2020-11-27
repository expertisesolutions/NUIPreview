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

namespace ElmSharp.Test
{
    class ColorSelectorTest1 : TestCaseBase
    {
        public override string TestName => "ColorSelectorTest1";
        public override string TestDescription => "To test basic operation of ColorSelector";

        public override void Run(Window window)
        {
            Background bg = new Background(window);
            bg.Color = Color.White;
            bg.Move(0, 0);
            bg.Resize(window.ScreenSize.Width, window.ScreenSize.Height);
            bg.Show();

            ColorSelector cs = new ColorSelector(window);

            Label label1 = new Label(window) {
                Text = string.Format("Selected Color={0}", cs.SelectedColor),
            };

            Label label2 = new Label(window);

            Label label3 = new Label(window);

            cs.ColorChanged += (object sender, ColorChangedEventArgs e) =>
            {
                label1.Text = string.Format("Selected Color={0}", cs.SelectedColor);
                label2.Text = string.Format("Old Color={0}", e.OldColor);
                label3.Text = string.Format("New Color={0}", e.NewColor);
            };

            cs.Resize(600, 600);
            cs.Move(0, 300);
            cs.Show();

            label1.Resize(600, 100);
            label1.Move(0, 0);
            label1.Show();

            label2.Resize(600, 100);
            label2.Move(0, 100);
            label2.Show();

            label3.Resize(600, 100);
            label3.Move(0, 200);
            label3.Show();
        }
    }
}