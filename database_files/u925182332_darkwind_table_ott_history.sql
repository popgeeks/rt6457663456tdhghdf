
-- --------------------------------------------------------

--
-- Table structure for table `ott_history`
--

CREATE TABLE `ott_history` (
  `ID` int(10) UNSIGNED NOT NULL,
  `GameID` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Board_Index` int(11) NOT NULL DEFAULT 0,
  `CardPlayed` varchar(60) NOT NULL DEFAULT '',
  `Results` varchar(500) DEFAULT NULL,
  `Timestamp` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
