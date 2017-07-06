using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DuplicatePhotoFinder.Tests
{
    [TestClass()]
    public class ExtensionMethodsTests
    {
        [TestMethod()]
        public void RootPathShouldBeValidPath()
        {
            // Arrange
            String path = @"D:\";
            bool isValid = false;

            // Act
            isValid = path.IsValidPath();

            // Assert
            Assert.IsTrue(isValid, "Root path is not a valid path when it should be.");
        }

        [TestMethod()]
        public void PathWithFolderNameShouldBeValidPath()
        {
            // Arrange
            String path = @"D:\TestFolder";
            bool isValid = false;

            // Act
            isValid = path.IsValidPath();

            // Assert
            Assert.IsTrue(isValid, "Path with folder name is not a valid path when it should be.");
        }

        [TestMethod()]
        public void PathWithInvalidCharactersShouldBeInvalidPath()
        {
            // Arrange
            String path = @"D:/TestFolder";
            bool isValid = false;

            // Act
            isValid = path.IsValidPath();

            // Assert
            Assert.IsFalse(isValid, "Path with invalid characters should not be a valid path.");
        }

        [TestMethod()]
        public void UNCPathShouldBeValidPath()
        {
            // Arrange
            String path = @"\\server\share";
            bool isValid = false;

            // Act
            isValid = path.IsValidPath();

            // Assert
            Assert.IsTrue(isValid, "UNC Path is not a valid path when it should be.");
        }
    }
}