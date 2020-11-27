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
using System.Collections.Generic;
using ElmSharp;

namespace ElmSharp.Test
{
    class CalendarTest1 : TestCaseBase
    {
        public override string TestName => "CalendarTest1";
        public override string TestDescription => "To test basic operation of Calendar";

        public override void Run(Window window)
        {
            Background bg = new Background(window);
            bg.Color = Color.White;
            bg.Move(0, 0);
            bg.Resize(window.ScreenSize.Width, window.ScreenSize.Height);
            bg.Show();

            Calendar calendar = new Calendar(window)
            {
                FirstDayOfWeek = DayOfWeek.Monday,
                WeekDayNames = new List<string>() { "S", "M", "T", "W", "T", "F", "S" },
                MinimumYear = DateTime.MinValue.Year,
                MaximumYear = DateTime.MaxValue.Year
            };

            IList<CalendarMark> marks = new List<CalendarMark>();

            var mark = calendar.AddMark("holiday", DateTime.Today, CalendarMarkRepeatType.Unique);
            marks.Add(mark);

            Label label1 = new Label(window)
            {
                Text = string.Format("WeekDayLabel.Count={0}", calendar.WeekDayNames.Count),
                Color = Color.Black,
            };

            Label label2 = new Label(window)
            {
                Text = string.Format("WeekDayLabel.FirstDayOfWeek={0}", calendar.FirstDayOfWeek),
                Color = Color.Black,
            };

            Label label3 = new Label(window)
            {
                Text = string.Format("WeekDayLabel.SelectedDate={0}", calendar.SelectedDate),
                Color = Color.Black,
            };

            var selectMode = new Label(window)
            {
                Text = string.Format("SelectMode = {0}", calendar.SelectMode),
                Color = Color.Black,
            };

            var addMark = new Button(window)
            {
                Text = "Add Mark"
            };

            var i = 1;

            addMark.Clicked += (s, e) =>
            {
                var newMark = calendar.AddMark("holiday", DateTime.Today.AddDays(i), CalendarMarkRepeatType.Unique);

                Console.WriteLine("Call Add Mark : " + DateTime.Today.AddDays(i));
                marks.Add(newMark);
                calendar.DrawMarks();
                i++;
            };

            var delMark = new Button(window)
            {
                Text = "Delete Mark"
            };

            delMark.Clicked += (s, e) =>
            {
                if (marks.Count > 0)
                {
                    calendar.DeleteMark(marks[0]);
                    marks.Remove(marks[0]);
                    calendar.DrawMarks();
                }
            };

            var changeMode = new Button(window)
            {
                Text = "Change Select Mode"
            };

            changeMode.Clicked += (s, e) =>
            {
                if (calendar.SelectMode == CalendarSelectMode.Always || calendar.SelectMode == CalendarSelectMode.Default)
                {
                    calendar.SelectMode = CalendarSelectMode.None;
                }
                else if (calendar.SelectMode == CalendarSelectMode.None)
                {
                    calendar.SelectMode = CalendarSelectMode.OnDemand;
                }
                else
                {
                    calendar.SelectMode = CalendarSelectMode.Always;
                }
                selectMode.Text = string.Format("SelectMode = {0}", calendar.SelectMode);
            };

            calendar.DateChanged += (object sender, DateChangedEventArgs e) =>
            {
                label1.Text = string.Format("Old.Day={0}, Month={1}, Year={2}", e.OldDate.Day, e.OldDate.Month, e.OldDate.Year);
                label2.Text = string.Format("New.Day={0}, Month={1}, Year={2}", e.NewDate.Day, e.NewDate.Month, e.NewDate.Year);
                label3.Text = string.Format("SelectedDate={0}", calendar.SelectedDate);
            };

            calendar.DisplayedMonthChanged += (object sender, DisplayedMonthChangedEventArgs e) =>
            {
                label3.Text = string.Format("Old Month={0}, New Month={1}", e.OldMonth, e.NewMonth);
            };

            var label4 = new Label(window)
            {
                Text = string.Format("Selectable={0}", calendar.Selectable),
                Color = Color.Black,
            };

            var changeSelectable = new Button(window)
            {
                Text = "Change Selectable"
            };

            calendar.Selectable = CalendarSelectable.Month;

            changeSelectable.Clicked += (s, e) =>
            {
                if (calendar.Selectable == CalendarSelectable.None)
                {
                    calendar.Selectable = CalendarSelectable.Year;
                }
                else if (calendar.Selectable == CalendarSelectable.Year)
                {
                    calendar.Selectable = CalendarSelectable.Month;
                }
                else if (calendar.Selectable == CalendarSelectable.Month)
                {
                    calendar.Selectable = CalendarSelectable.Day;
                }
                else
                {
                    calendar.Selectable = CalendarSelectable.None;
                }
                label4.Text = string.Format("Selectable={0}", calendar.Selectable);
            };

            var setTime = new Button(window)
            {
                Text = "Set 2015,1,1",
            };

            setTime.Clicked += (s, e) =>
            {
                calendar.SelectedDate = new DateTime(2015, 1, 1);
            };

            calendar.Resize(600, 600);
            calendar.Move(0, 150);
            calendar.Show();

            label1.Resize(600, 30);
            label1.Move(0, 0);
            label1.Show();

            label2.Resize(600, 30);
            label2.Move(0, 30);
            label2.Show();

            label3.Resize(600, 30);
            label3.Move(0, 60);
            label3.Show();

            selectMode.Resize(600, 30);
            selectMode.Move(0, 90);
            selectMode.Show();

            addMark.Resize(600, 100);
            addMark.Move(0, 750);
            addMark.Show();

            delMark.Resize(600, 100);
            delMark.Move(0, 850);
            delMark.Show();

            changeMode.Resize(600, 100);
            changeMode.Move(0, 950);
            changeMode.Show();

            label4.Resize(600, 30);
            label4.Move(0, 1050);
            label4.Show();

            changeSelectable.Resize(600, 100);
            changeSelectable.Move(0, 1080);
            changeSelectable.Show();

            setTime.Resize(600, 100);
            setTime.Move(0, 1180);
            setTime.Show();
        }
    }
}