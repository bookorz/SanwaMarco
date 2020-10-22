Function ILPT_UNLOAD();
    SETVAR("@device", "DNM01");
    '檢查 Port 資料
	Return ("Port Error:@<arg1>", "('@<arg1>' <> 'P3') AND ('@<arg1>' <> 'P4')");
    'CHECK SENSORS
    SETVAR("@io", DECODE("@<arg1>", "P3", "401;403;404;407;304;306;702", "P4", "409;411;412;415;312;314;703"));
	SETVARS("@interval", "200", "@retry_count", "1", "@values", "1111111");
    API("I7565DNM_CHECK_IOS", True);
    'Return ("Port sensors error", "@<I7565DNM_CHECK_IOS_RETURN> = 1111110");
    'RESET ALL COMMAND
    SETVAR("@io", DECODE("@<arg1>", "P3", "1100;1101;1102;1103;1104;1105;1106;1107;1108;1109;1110;1111", "P4", "1200;1201;1202;1203;1204;1205;1206;1207;1208;1209;1210;1211"));
    SETVAR("@value", "0;0;0;0;0;0;0;0;0;0;0;0");
    API("I7565DNM_SETIOS", True);
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
    SETVAR("@io", DECODE("@<arg1>", "P3", "1102;1103", "P4", "1202;1203"));
    SETVAR("@value", "1;0");
    API("I7565DNM_SETIOS", True);
    DELAY(2000);

    SETVAR("@io", DECODE("@<arg1>", "P3", "400", "P4", "408"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

    '26 port 須將另一個點位 On
    SETVAR("@io", DECODE("@<arg1>", "P3", "1103", "P4", "1203"));
    SETVAR("@value", "1");
    API("I7565DNM_SETIOS", True);

    DELAY(1000);

    'SETVAR("@io", DECODE("@<arg1>", "P3", "1102;1103", "P4", "1202;1203"));
    'SETVAR("@value", "0;0");
    'API("I7565DNM_SETIOS", True);

    '避免因為氣壓過大，電磁閥跳回來
    SETVAR("@io", DECODE("@<arg1>", "P3", "400", "P4", "408"));
	SETVARS("@interval", "200", "@retry_count", "10", "@values", "1");
    API("I7565DNM_CHECK_IOS", True);

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