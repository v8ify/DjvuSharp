%module djvulibre

%{
    #include "include/ddjvuapi.h"
%}

%typemap(csclassmodifiers) SWIGTYPE "internal class"
%pragma(csharp) moduleclassmodifiers="internal class"

%include <windows.i>
%include "include/ddjvuapi.h"