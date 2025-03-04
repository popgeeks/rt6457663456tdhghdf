
-- --------------------------------------------------------

--
-- Table structure for table `playerleaderboard_lu`
--

CREATE TABLE `playerleaderboard_lu` (
  `PlayerLeaderboardLU_ID` int(11) NOT NULL,
  `PlayerLeaderboardLU_Description` varchar(256) NOT NULL,
  `PlayerLeaderboardLU_Stamp` timestamp NOT NULL DEFAULT '0000-00-00 00:00:00' ON UPDATE current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
