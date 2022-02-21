using MvvmCross.ViewModels;
using XamarinSocial.Resources.Strings;

namespace XamarinSocial.ViewModels
{
    public class BaseCellModel
    {
        public string this[string index] => Strings.ResourceManager.GetString(index);
    }
}
