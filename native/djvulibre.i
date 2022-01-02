%module djvulibre

%{
    #include "include/ddjvuapi.h"
%}


// This function exists as a macro in the header file
// As such, swig does not expand it automatically
// Hence we need to trick it into expanding by writing
// a stub function
void ddjvu_document_release(ddjvu_document_t*);

void ddjvu_page_release(ddjvu_page_t*);


// These function return char* which was malloced
// we need to free the memory manually

%newobject ddjvu_document_get_dump;

%newobject ddjvu_document_get_pagedump;

%newobject ddjvu_document_get_pagedump_json;

%newobject ddjvu_document_get_filedump;

%newobject ddjvu_document_get_filedump_json;


%include <windows.i>
%include "include/ddjvuapi.h"
