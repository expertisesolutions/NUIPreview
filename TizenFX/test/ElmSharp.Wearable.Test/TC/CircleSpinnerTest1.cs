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

using ElmSharp.Wearable;

namespace ElmSharp.Test.TC
{
    class CircleSpinnerTest1 : TestCaseBase
    {
        public override string TestName => "CircleSpinnerTest1";
        public override string TestDescription => "To test basic operation of Circle Spinner with no AngleRatio";

        public override void Run(Window window)
        {
            Log.Debug(TestName, "CircleSpinnerTest run");
            Conformant conformant = new Conformant(window);
            conformant.Show();

            Layout layout = new Layout(window);
            layout.SetTheme("layout", "circle", "spinner");
            conformant.SetContent(layout);

            var surface = new CircleSurface(conformant);
            CircleSpinner spn1 = new CircleSpinner(conformant, surface)
            {
                Text = "Spinner Test",
                LabelFormat = "%d Value",
                Style = "circle",
                Minimum = 0,
                Maximum = 100,
                Value = 0,
                Step = 10,
                Interval = 0.5,
                AlignmentX = -1,
                AlignmentY = 1,
                WeightX = 1,
                WeightY = 1
            };
            ((IRotaryActionWidget)spn1).Activate();
            spn1.AddSpecialValue(50, "50 match !!!!");
            layout.SetContent(spn1);

            Button btn = new Button(layout) {
                Text = "OK",
                Style = "bottom",
            };

            btn.Clicked += (s, e) =>
            {
                spn1.IsEnabled = false;
                spn1.Text = "100 match";
                spn1.Value = 0;
            };

            layout.SetPartContent("elm.swallow.btn", btn);
            layout.SetPartText("elm.text", "Set value");
        }
    }
}
