using System;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace Missio.Navigation
{
    public class ApplicationPages
    {
        public Page[] AvailableViews { get; }

        public ApplicationPages([NotNull] Page[] availableViews)
        {
            AvailableViews = availableViews ?? throw new ArgumentNullException(nameof(availableViews));
        }
    }
}