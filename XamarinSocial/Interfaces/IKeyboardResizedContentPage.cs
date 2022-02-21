using System;

namespace XamarinSocial.Interfaces
{
    public interface IKeyboardResizedContentPage
    {
        event EventHandler EntryUnfocused;

        //If needed to change XamarinForms gesture recognizer behavior (iOS), set CancelsTouchesInView to false
        bool CancelsTouchesInView { get; set; }
    }
}
