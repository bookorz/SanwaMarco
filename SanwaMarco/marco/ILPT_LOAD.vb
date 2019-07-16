Function ILPT_LOAD();
    SETVAR("@device", "DNM01");
    '檢查 Port 資料
	Return ("Port Error:@<arg1>", "'@<arg1>' not in ('P3','P4')");
    'CHECK SENSORS
    SETVAR("@io", DECODE("@<arg1>", "P3", "400;402;405;406;305;307;702", "P4", "408;410;413;414;313;315;703"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1111110");
    API("I7565DNM_CHECK_IOS", True);
    'Return ("Port sensors error", "@<I7565DNM_CHECK_IOS_RETURN> = 1111110");
    'ILPT_DOCK
    SETVAR("@io", DECODE("@<arg1>", "P3", "1100", "P4", "1200"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "304", "P4", "312"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1100", "P4", "1200"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);

    'ILPT_CLAMP
    SETVAR("@io", DECODE("@<arg1>", "P3", "1108", "P4", "1208"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "306", "P4", "314"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1108", "P4", "1208"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);

    'ILPT_VAC_ON
    SETVAR("@io", DECODE("@<arg1>", "P3", "1403", "P4", "1411"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "702", "P4", "703"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1403", "P4", "1411"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);

    'ILPT_LATCH_RELEASE
    SETVAR("@io", DECODE("@<arg1>", "P3", "1103", "P4", "1203"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "401", "P4", "409"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1103", "P4", "1203"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);

    'ILPT_BACKWARD(後退)
    SETVAR("@io", DECODE("@<arg1>", "P3", "1104", "P4", "1204"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "403", "P4", "411"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1104", "P4", "1204"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);

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

    'ILPT_LEFT
    SETVAR("@io", DECODE("@<arg1>", "P3", "1106", "P4", "1206"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "404", "P4", "412"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    SETVAR("@io", DECODE("@<arg1>", "P3", "1106", "P4", "1206"));
    SETVAR("@value", "0");
    API("I7565DNM_SETIOS", True);
End Function;