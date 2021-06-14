#include <iostream>
#include "LittleAlgorithm.h"
using namespace std;
int main()
{
	try {
		Algorithm* method = new Algorithm();
		method = new LittleAlgorithm();
		string fileadress = "C:\\Users\\user\\Desktop\\Litle\\solvethis.txt";
		char buf[255];
		strcpy_s(buf, fileadress.c_str());
		method->LoadData(buf);
		method->Run();
	}
	catch (char* err) {
		cout << "Exception: \n" << err << endl;
	}
	system("pause");
}