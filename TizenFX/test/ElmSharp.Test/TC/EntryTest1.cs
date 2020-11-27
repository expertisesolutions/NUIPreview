﻿/*
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
    class EntryTest1 : TestCaseBase
    {
        public override string TestName => "EntryTest1";
        public override string TestDescription => "To test basic operation of Entry";

        public override void Run(Window window)
        {
            Background bg = new Background(window)
            {
                AlignmentX = -1,
                AlignmentY = -1,
                WeightX = 1,
                WeightY = 1,
                Color = Color.White
            };
            bg.Show();
            window.AddResizeObject(bg);

            Conformant conformant = new Conformant(window);
            conformant.Show();
            Box box = new Box(window);
            conformant.SetContent(box);
            box.Show();

            Entry entry1 = new Entry(window)
            {
                AlignmentX = -1,
                AlignmentY = 0,
                WeightX = 1,
                WeightY = 1,
                IsSingleLine = true,
            };
            entry1.SetPartText("guide", "<span color=#999999>Single Line</span>");

            Entry entry2 = new Entry(window)
            {
                AlignmentX = -1,
                AlignmentY = 0,
                WeightX = 1,
                WeightY = 1,
                IsPassword = true
            };
            entry2.SetPartText("guide", "<span color=#999999>Password</span>");

            Entry entry3 = new Entry(window)
            {
                IsSingleLine = true,
                Scrollable = true,
            };
            entry3.SetPartText("guide", "<span color=#808080FF font_size=28 align=left valign=top wrap=mixed>Enter a System.Double</span>");
            entry3.Geometry = new Rect(0, 100, 720, 37);
            entry3.Show();

            var capital1 = new Entry(window)
            {
                AlignmentX = -1,
                AlignmentY = 0,
                WeightX = 1,
                WeightY = 1,
                Text = "Auto Capital : Word",
                AutoCapital = AutoCapital.Word
            };

            var capital2 = new Entry(window)
            {
                AlignmentX = -1,
                AlignmentY = 0,
                WeightX = 1,
                WeightY = 1,
                Text = "Auto Capital : Sentence",
                AutoCapital = AutoCapital.Sentence
            };

            var capital3 = new Entry(window)
            {
                AlignmentX = -1,
                AlignmentY = 0,
                WeightX = 1,
                WeightY = 1,
                Text = "Auto Capital : All",
                AutoCapital = AutoCapital.All
            };

            var getText = new Entry(window)
            {
                AlignmentX = -1,
                AlignmentY = 0,
                WeightX = 1,
                WeightY = 1,
                Text = "get cursor content test",
            };

            var label = new Label(window)
            {
                AlignmentX = -1,
                AlignmentY = 0,
                WeightX = 1,
                WeightY = 1,
            };

            getText.CursorChanged += (s, e) =>
            {
                label.Text = getText.GetCursorContent();
            };

            //var autoSave = new Entry(window)
            //{
            //    AlignmentX = -1,
            //    AlignmentY = 0,
            //    WeightX = 1,
            //    WeightY = 1,
            //    Text = "Auto Save",
            //    IsAutoSave = true
            //};

            var btn = new Button(window)
            {
            };

            box.PackEnd(entry1);
            box.PackEnd(entry2);
            box.PackEnd(capital1);
            box.PackEnd(capital2);
            box.PackEnd(capital3);
            box.PackEnd(getText);
            box.PackEnd(label);

            entry1.Show();
            entry2.Show();
            capital1.Show();
            capital2.Show();
            capital3.Show();
            getText.Show();
            label.Show();
        }
    }
}