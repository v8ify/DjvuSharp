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

    echo "Deleting Generated folder from DjvuSharp..." ;
    echo ;
    rm -rf $cwd/../DjvuSharp/Generated ;
    mkdir -p $cwd/../DjvuSharp/Generated ;
    
    swig -csharp -c++ -outdir $cwd/../DjvuSharp/Generated/ -namespace Djvulibre.Internal $cwd/djvulibre.i
    echo "Done..." ;

    echo "Creating shared library..."

    g++ -shared -pthread -fpic -o libdjvulibre.so $cwd/djvulibre_wrap.cxx $cwd/libs/linux-64/libdjvulibre.a ;
    echo ;

    echo "Successfully completed!" ;

elif [[ "$OSTYPE" == "darwin"* ]]; then
	echo "Sorry MacOS version is not available yet."
fi

cd "$cwd"