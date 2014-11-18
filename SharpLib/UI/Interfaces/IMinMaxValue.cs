
namespace SharpLib2D.UI.Interfaces
{
    interface IMinMaxValue
    {
        /// <summary>
        /// The control's value.
        /// </summary>
        double Value { set; get; }

        /// <summary>
        /// The control's minimum value.
        /// </summary>
        double MinValue { set; get; }

        /// <summary>
        /// The control's maximum value.
        /// </summary>
        double MaxValue { set; get; }
    }
}
