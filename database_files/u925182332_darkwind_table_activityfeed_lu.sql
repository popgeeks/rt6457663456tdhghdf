
-- --------------------------------------------------------

--
-- Table structure for table `activityfeed_lu`
--

CREATE TABLE `activityfeed_lu` (
  `idActivityFeed_LU` int(11) NOT NULL,
  `ActivityFeedDescription` varchar(100) NOT NULL,
  `ActivityFeedImage` varchar(250) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `activityfeed_lu`
--

INSERT INTO `activityfeed_lu` (`idActivityFeed_LU`, `ActivityFeedDescription`, `ActivityFeedImage`) VALUES
(1, '1st Place Trophy', '/_common/images/sprites/trophy-gold.png'),
(2, '2nd Place Trophy', '/_common/images/sprites/trophy-silver.png'),
(3, '3rd Place Trophy', '/_common/images/sprites/trophy-bronze.png'),
(4, 'Card', '/_common/images/sprites/up-cards.png'),
(5, 'Player Details', '/_common/images/sprites/up-player.png'),
(6, 'Achievements', '/_common/images/sprites/up-achievements.png'),
(7, 'Level Up', '/_common/images/sprites/up-wish.png');
