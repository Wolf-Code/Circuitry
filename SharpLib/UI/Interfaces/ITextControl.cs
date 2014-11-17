
namespace SharpLib2D.UI.Interfaces
{
    interface ITextControl
    {
        string Text { get; }

        /// <summary>
        /// Sets the object's text.
        /// </summary>
        /// <param name="NewText">The new text for this object.</param>
        void SetText( string NewText );
    }
}
