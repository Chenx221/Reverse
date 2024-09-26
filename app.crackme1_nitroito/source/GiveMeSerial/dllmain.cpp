// dllmain.cpp
#include "pch.h"
#include <Windows.h>

extern "C" __declspec(dllexport) void ShowDataAndCopy(const char* data) {
    const size_t len = strlen(data) + 1;
    HGLOBAL hMem = GlobalAlloc(GMEM_MOVEABLE, len);
    memcpy(GlobalLock(hMem), data, len);
    GlobalUnlock(hMem);
    OpenClipboard(0);
    EmptyClipboard();
    SetClipboardData(CF_TEXT, hMem);
    CloseClipboard();
}
