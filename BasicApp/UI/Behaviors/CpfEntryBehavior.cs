using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace BasicApp.UI.Behaviors
{
    public class CpfEntryBehavior : Behavior<Entry>
    {

        void HandleCpfEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            var entry = (sender as Entry);
            if (!entry.IsFocused)
                return;

            if (String.IsNullOrEmpty(e.NewTextValue))
                return;

            var cpfOnlyNumbers = Regex.Replace(e.NewTextValue, @"[^0-9]", "");

            if (cpfOnlyNumbers.Length > 11)
                entry.Text = e.OldTextValue;
            else
                entry.Text = cpfOnlyNumbers;

        }

        void HandleCpfEntryFocused(object sender, EventArgs e)
        {
            var entry = (sender as Entry);

            if (String.IsNullOrEmpty(entry.Text))
                return;

            var cpfOnlyNumbers = Regex.Replace(entry.Text, @"[^0-9]", "");

            entry.Text = cpfOnlyNumbers;
            entry.TextColor = Color.Default;
        }

        void HandleCpfEntryUnfocused(object sender, EventArgs e)
        {
            var entry = (sender as Entry);

            try
            {
                if (String.IsNullOrEmpty(entry.Text))
                    return;

                entry.Text = Regex.Replace(entry.Text, @"[^0-9]", ""); //Remove previous caracters
                entry.Text = String.Format(@"{0:\000\.000\.000\-00}", Convert.ToInt64(entry.Text)).Substring(1); //Substring(1) to remove a leading 0

            }
            catch (FormatException)
            {
                entry.TextColor = Color.Red;
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.Focused += HandleCpfEntryFocused;
            bindable.Unfocused += HandleCpfEntryUnfocused;
            bindable.TextChanged += HandleCpfEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Focused -= HandleCpfEntryFocused;
            bindable.Unfocused -= HandleCpfEntryUnfocused;

        }
    }
}
