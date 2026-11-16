// Y6 - Chay song song theo lop test (fixture): cac lop chay cung luc, cac ca trong cung mot lop van chay lan luot.
// Moi lop co driver rieng (field thuong trong BaseTest) nen khong dung chung trinh duyet.
[assembly: Parallelizable(ParallelScope.Fixtures)]

// So luong toi da cung luc. May co 12 nhan nhung de 4 cho do nang may (moi luong la mot Chrome).
[assembly: LevelOfParallelism(4)]
