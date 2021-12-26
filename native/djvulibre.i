%module djvulibre

%{
    #include "include/ddjvuapi.h"
%}

%typemap(csclassmodifiers) SWIGTYPE "internal class"
%typemap(csclassmodifiers) SWIGTYPE_p_ddjvu_job_s "internal class"
%pragma(csharp) moduleclassmodifiers="internal class"

%include <windows.i>
%include "include/ddjvuapi.h"