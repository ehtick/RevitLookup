using System.Windows.Media;

namespace RevitLookup.Abstractions.ViewModels.Visualization;

/// <summary>
///     Defines a contract that represents the data for curve loop visualization.
/// </summary>
public interface ICurveLoopVisualizationViewModel
{
    /// <summary>
    ///     Gets the minimum thickness of the curve loop.
    /// </summary>
    double MinThickness { get; }

    /// <summary>
    ///     Gets or sets the diameter of the curve loop.
    /// </summary>
    double Diameter { get; set; }

    /// <summary>
    ///     Gets or sets the transparency level of visualization.
    /// </summary>
    double Transparency { get; set; }

    /// <summary>
    ///     Gets or sets the color of the curve loop surface.
    /// </summary>
    Color SurfaceColor { get; set; }

    /// <summary>
    ///     Gets or sets the color of the curve loop curve.
    /// </summary>
    Color CurveColor { get; set; }

    /// <summary>
    ///     Gets or sets the color of the curve loop direction indicators.
    /// </summary>
    Color DirectionColor { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether to show the curve loop surface.
    /// </summary>
    bool ShowSurface { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether to show the curve loop curve.
    /// </summary>
    bool ShowCurve { get; set; }

    /// <summary>
    ///     Gets or sets a value indicating whether to show the curve loop direction indicators.
    /// </summary>
    bool ShowDirection { get; set; }

    /// <summary>
    ///     Registers the visualization server for the specified curve loop.
    /// </summary>
    /// <param name="curveLoop">The Revit <c>CurveLoop</c> to visualize.</param>
    void RegisterServer(object curveLoop);

    /// <summary>
    ///     Unregisters the curve loop visualization server.
    /// </summary>
    void UnregisterServer();
}
