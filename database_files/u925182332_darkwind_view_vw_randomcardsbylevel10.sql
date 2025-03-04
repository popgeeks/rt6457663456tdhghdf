
-- --------------------------------------------------------

--
-- Structure for view `vw_randomcardsbylevel10`
--
DROP TABLE IF EXISTS `vw_randomcardsbylevel10`;

CREATE ALGORITHM=UNDEFINED DEFINER=`u925182332_darkwind`@`localhost` SQL SECURITY DEFINER VIEW `vw_randomcardsbylevel10`  AS SELECT `cards`.`id` AS `ID`, rand() AS `rand()` FROM `cards` WHERE `cards`.`level` = 10 ORDER BY rand() ASC LIMIT 0, 10 ;
