/*
 * Source: https://github.com/xamarin/xamarin-forms-samples/blob/master/UserInterface/VisualDemos/VisualDemos/ImageResourceExtension.cs
 */
namespace Classification {
    using System;
    using System.Reflection;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [ContentProperty(nameof(Source))]
    public class ImageResourceExtension : IMarkupExtension {
        public string Source { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider) {
            if(Source == null) {
                return null;
            }

            var imageSource = ImageSource.FromResource(Source, typeof(ImageResourceExtension).GetTypeInfo().Assembly);
            return imageSource;
        }
    }
}