
-- --------------------------------------------------------

--
-- Table structure for table `playerleaderboard`
--

CREATE TABLE `playerleaderboard` (
  `PlayerLeaderboardID` int(11) NOT NULL,
  `PlayerLeaderboardUserIDFK` int(11) NOT NULL,
  `PlayerLeaderboardLeaderboardIDFK` int(11) NOT NULL,
  `PlayerLeaderboardUserScore` int(11) NOT NULL DEFAULT 1500
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
