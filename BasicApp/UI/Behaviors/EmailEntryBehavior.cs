using System;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace BasicApp.UI.Behaviors
{
    public class EmailEntryBehavior : Behavior<Entry>
    {
        void HandleEmailEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Regex.IsMatch(e.NewTextValue, @"^\w+\@\w+\.(com|net)(\.\w+)?$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)))
            {
                (sender as Entry).TextColor = Color.Red;
            }
            else
            {
                (sender as Entry).TextColor = Color.Default;
            }
        }

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += HandleEmailEntryTextChanged;
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= HandleEmailEntryTextChanged;
        }
    }
}
