!#/bin/bash

cwd=$PWD

if [[ "$OSTYPE" == "linux-gnu"* ]]; then
    echo "Building linux-x64 native library..." ;
    echo ;
	rm -rf out/build/linux-x64 ;
	mkdir -p out/build/linux-x64 ;
    cd out/build/linux-x64 ;
    
    echo "Running swig and creating wrapper..." ;
    echo ;
    
    # swig -csharp -c++ -outdir ../DjvuSharp/Generated/ -namespace Djvulibre.Internal djvulibre.i
    g++ -shared -pthread -fpic -o libdjvulibre.so $cwd/djvulibre_wrap.cxx $cwd/libs/linux-64/libdjvulibre.a


elif [[ "$OSTYPE" == "darwin"* ]]; then
	echo "Sorry MacOS version is not available yet."
fi

cd "$cwd"