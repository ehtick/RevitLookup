using RevitLookup.ServiceDefaults.FileSystem;

namespace RevitLookup.Tests.Unit;

public sealed class PathExtensionsTests
{
    [Test]
    public async Task AppendPath_MultipleSegments_CombinesInOrder()
    {
        // Arrange
        const string root = "root";

        // Act
        var combined = root.AppendPath("a", "b", "c");

        // Assert
        await Assert.That(combined).IsEqualTo(Path.Combine("root", "a", "b", "c"));
    }

    [Test]
    public async Task AppendPath_SingleSegment_CombinesWithSource()
    {
        // Arrange
        const string root = "root";

        // Act
        var combined = root.AppendPath("child");

        // Assert
        await Assert.That(combined).IsEqualTo(Path.Combine("root", "child"));
    }
}
