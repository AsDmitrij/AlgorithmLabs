#include <iostream>
#include "LittleAlgorithm.h"

using namespace std;
void main()
{
	try {
		Algorithm* method = new Algorithm();
		switch (0){
			case 0: method = new LittleAlgorithm(); break;
		}
	    char hello[] = "hello, world!";
		char* cch= hello;
		method->LoadData(cch);
		method->Run();
	}
	catch (char* err) {
		cout << "Exception: \n" << err << endl;
	}
}

