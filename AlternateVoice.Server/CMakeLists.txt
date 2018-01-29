# Setup base project
cmake_minimum_required(VERSION 3.5)
project(AlternateVoice VERSION 1.0.0 LANGUAGES CXX)

# Make sure to install libraries into correct locations
include(GNUInstallDirs)

# Setup basic C++ and project attributes
set(CMAKE_CXX_STANDARD 11)

set(CMAKE_INSTALL_RPATH "${CMAKE_INSTALL_PREFIX}/lib")
set(CMAKE_INSTALL_RPATH_USE_LINK_PATH TRUE)

# Include /usr/local/lib/pkgconfig into pkg-config search path
set(ENV{PKG_CONFIG_PATH} "${CMAKE_INSTALL_PREFIX}/lib/pkgconfig/:$ENV{PKG_CONFIG_PATH}")

# Add targets
enable_testing()

add_subdirectory(src)
add_subdirectory(test)
