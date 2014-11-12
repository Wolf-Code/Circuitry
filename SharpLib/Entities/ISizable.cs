using OpenTK;

namespace SharpLib2D.Entities
{
    public interface ISizable
    {
        Vector2 Size { get; }
        float Width { get; }
        float Height { get; }

        /// <summary>
        /// Sets the size of this object.
        /// </summary>
        /// <param name="NewSize">The new size.</param>
        void SetSize( Vector2 NewSize );

        /// <summary>
        /// Sets the size of this object.
        /// </summary>
        /// <param name="NewWidth">The new width.</param>
        /// <param name="NewHeight">The new height.</param>
        void SetSize( float NewWidth, float NewHeight );

        /// <summary>
        /// Sets the width of this object.
        /// </summary>
        /// <param name="NewWidth">The new width.</param>
        void SetWidth( float NewWidth );

        /// <summary>
        /// Sets the height of this object.
        /// </summary>
        /// <param name="NewHeight">The new height.</param>
        void SetHeight( float NewHeight );
    }
}
