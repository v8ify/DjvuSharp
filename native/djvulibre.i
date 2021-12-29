%module djvulibre

%{
    #include "include/ddjvuapi.h"
%}


// This function exists as a macro in the header file
// As such, swig does not expand it automatically
// Hence we need to trick it into expanding by writing
// a stub function
void ddjvu_document_release(ddjvu_document_t*);


%include <windows.i>
%include "include/ddjvuapi.h"
