Function ILPT_UNLOAD();
    SETVAR("@device", "DNM01");
    '檢查 Port 資料
	Return ("Port Error:@<arg1>", "'@<arg1>' not in ('P3','P4')");
    'CHECK SENSORS
    SETVAR("@io", DECODE("@<arg1>", "P3", "401;403;404;407;304;306;702", "P4", "409;411;412;415;312;314;703"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1111111");
    API("I7565DNM_CHECK_IOS", True);
    'Return ("Port sensors error", "@<I7565DNM_CHECK_IOS_RETURN> = 1111110");
    'ILPT_RIGHT
    SETVAR("@io", DECODE("@<arg1>", "P3", "1107", "P4", "1207"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "405", "P4", "413"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1107", "P4", "1207"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'ILPT_CLOSE(密封)
    SETVAR("@io", DECODE("@<arg1>", "P3", "1110", "P4", "1210"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "406", "P4", "414"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1110", "P4", "1210"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'ILPT_FORWARD(前進)
    SETVAR("@io", DECODE("@<arg1>", "P3", "1105", "P4", "1205"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "402", "P4", "410"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1105", "P4", "1205"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'ILPT_LATCH_FIX
    SETVAR("@io", DECODE("@<arg1>", "P3", "1102", "P4", "1202"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "400", "P4", "408"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1102", "P4", "1202"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'ILPT_VAC_OFF
    SETVAR("@io", DECODE("@<arg1>", "P3", "1402", "P4", "1410"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "702", "P4", "703"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "0");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1402", "P4", "1410"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'ILPT_UNCLAMP
    SETVAR("@io", DECODE("@<arg1>", "P3", "1109", "P4", "1209"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "307", "P4", "315"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1109", "P4", "1209"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
    'ILPT_UNDOCK
    SETVAR("@io", DECODE("@<arg1>", "P3", "1101", "P4", "1201"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "305", "P4", "313"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1101", "P4", "1201"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
End Function;