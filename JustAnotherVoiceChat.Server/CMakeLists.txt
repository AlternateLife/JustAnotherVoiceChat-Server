# Setup base project
cmake_minimum_required(VERSION 3.5)
project(JustAnotherVoiceChat VERSION 1.0.0 LANGUAGES CXX)

# Make sure to install libraries into correct locations
include(GNUInstallDirs)

# Setup basic C++ and project attributes
set(CMAKE_CXX_STANDARD 11)
set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -Wall -pedantic")

set(CMAKE_POSITION_INDEPENDENT_CODE ON)

if(WIN32)
  add_definitions(-DNOMINMAX)
endif(WIN32)

include_directories(include)
include_directories(thirdparty/enet/include)
include_directories(thirdparty/cereal/include)
include_directories(thirdparty/linalg)

# Add targets
enable_testing()

add_subdirectory(src)
add_subdirectory(tests)
add_subdirectory(thirdparty/enet)
