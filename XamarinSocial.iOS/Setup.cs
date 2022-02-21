using MvvmCross.Forms.Platforms.Ios.Core;
using MvvmCross.IoC;
using UIKit;
using XamarinSocial.Forms;

namespace XamarinSocial.iOS
{
    public class Setup : MvxFormsIosSetup<CoreApp, App>
    {
        protected override void InitializeFirstChance()
        {
            CreatableTypes()
                .EndingWith("Helper")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            base.InitializeFirstChance();
        }
    }
}