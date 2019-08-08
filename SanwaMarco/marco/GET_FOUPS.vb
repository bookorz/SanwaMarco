Function GET_FOUPS();
    SETVAR("@foups", "2");' Robot 在席因為沒有sensor先不處理
    SETVAR("@device", "DNM01");
    API("I7565DNM_REFRESH", True);
    'A1在席確認 
    SETVAR("@io", "101;102;103");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "111", "0", "000", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'A2在席確認 
    SETVAR("@io", "201;202;203");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "111", "0", "000", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'B1在席確認 
    SETVAR("@io", "301;302;303");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "111", "0", "000", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'B2在席確認 
    SETVAR("@io", "309;310;311");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "111", "0", "000", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 1-1 在席確認 
    SETVAR("@io", "500;501");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 1-2 在席確認 
    SETVAR("@io", "504;505");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 1-3 在席確認 
    SETVAR("@io", "508;509");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 1-4 在席確認 
    SETVAR("@io", "512;513");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 1-5 在席確認 
    SETVAR("@io", "808;809");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 2-1 在席確認 
    SETVAR("@io", "502;503");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 2-2 在席確認 
    SETVAR("@io", "506;507");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 2-3 在席確認 
    SETVAR("@io", "510;511");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 2-4 在席確認 
    SETVAR("@io", "514;515");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 2-5 在席確認 
    SETVAR("@io", "810;811");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 3-1 在席確認 
    SETVAR("@io", "600;601");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 3-2 在席確認 
    SETVAR("@io", "604;605");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 3-3 在席確認 
    SETVAR("@io", "608;609");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 3-4 在席確認 
    SETVAR("@io", "812;813");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 4-1 在席確認 
    SETVAR("@io", "602;603");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 4-2 在席確認 
    SETVAR("@io", "606;607");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 4-3 在席確認 
    SETVAR("@io", "610;611");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 4-4 在席確認 
    SETVAR("@io", "814;815");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    RETVAL("@<foups>");'回傳最終結果
End Function;