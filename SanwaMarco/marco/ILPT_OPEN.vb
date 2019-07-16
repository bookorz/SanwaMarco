﻿Function ILPT_LOAD();
    SETVAR("@device", "DNM01");
    '檢查 Port 資料
	Return ("Port Error:@<arg1>", "'@<arg1>' not in ('P3','P4')");
    'CHECK SENSORS
    SETVAR("@io", DECODE("@<arg1>", "P3", "400;402;405;406;305;307;702;304;306;401;403", "P4", "408;410;413;414;313;315;703;312;314;409;411"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "00110011111");
    API("I7565DNM_CHECK_IOS", True);
    'Return ("Port sensors error", "@<I7565DNM_CHECK_IOS_RETURN> = 1111110");
    'ILPT_OPEN(鬆開)
    SETVAR("@io", DECODE("@<arg1>", "P3", "1111", "P4", "1211"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "407", "P4", "415"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1111", "P4", "1211"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
End Function;