using System;
using Xamarin.Forms;

namespace BasicApp.UI.Behaviors
{
    public class CpfEntryBehavior : Behavior<Entry>
    {

        void HandleCpfEntryFocused(object sender, EventArgs e)
        {
            var entry = (sender as Entry);
            entry.TextColor = Color.Default;
        }

        void HandleCpfEntryUnfocused(object sender, EventArgs e)
        {
            var entry = (sender as Entry);

            try
            {
                entry.Text = entry.Text.Replace(".", "").Replace("-", ""); //Remove previous caracters
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
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.Focused -= HandleCpfEntryFocused;
            bindable.Unfocused -= HandleCpfEntryUnfocused;
        }
    }
}
