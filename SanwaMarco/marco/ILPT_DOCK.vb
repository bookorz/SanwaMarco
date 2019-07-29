Function ILPT_DOCK();
    SETVAR("@device", "DNM01");
    '檢查 Port 資料
	Return ("Port Error:@<arg1>", "('@<arg1>' <> 'P3') AND ('@<arg1>' <> 'P4')");
    'CHECK SENSORS
    SETVAR("@io", DECODE("@<arg1>", "P3", "400;402;405;406;305;307;702", "P4", "408;410;413;414;313;315;703"));
	SETVARS("@interval", "200", "@retry_count", "1", "@values", "1111110");
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
End Function;