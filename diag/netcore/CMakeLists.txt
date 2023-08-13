cmake_minimum_required(VERSION 3.0)

project(usb_diag_netcore)
include(../../../../../include/wd.cmake)

set(SRCS
    ../AssemblyInfo.cs
    ../FormChangeSettings.cs
    ../UsbSample.cs)

    add_custom_target(usb_diag_netcore ALL
        COMMAND dotnet publish ${CMAKE_CURRENT_SOURCE_DIR}/csharp_usb_diag_netcore.csproj 
            -p:Configuration=${CMAKE_BUILD_TYPE}
            -r ${RUNTIME_ID} --self-contained true
            -p:BinaryOutDir=${BinaryOutDir}
            -p:ARCH=${ARCH}
        SOURCES ${SRCS}
        COMMENT "Building USB diag with .NET CORE 6"
      )
