Function GET_FOUPS();
    SETVAR("@foups", "3");' Robot 在席因為沒有sensor先不處理
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
    'SHELF 1-6 在席確認 
    SETVAR("@io", "800;801");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 1-7 在席確認 
    SETVAR("@io", "804;805");
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
    'SHELF 2-6 在席確認 
    SETVAR("@io", "802;803");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 2-7 在席確認 
    SETVAR("@io", "806;807");
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
    SETVAR("@io", "2200;2201");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 3-5 在席確認 
    SETVAR("@io", "2204;2205");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 3-6 在席確認 
    SETVAR("@io", "2208;2209");
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
    SETVAR("@io", "2202;2203");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 4-5 在席確認 
    SETVAR("@io", "2206;2207");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    'SHELF 4-6 在席確認 
    SETVAR("@io", "2210;2211");
    API("I7565DNM_READIOS", False);	
    SETVAR("@temp", DECODE("@<I7565DNM_READIOS_RETURN>", "11", "0", "00", "1", "2");
    SETVAR("@foups", "@<foups>@<temp>");
    RETVAL("@<foups>");'回傳最終結果
End Function;