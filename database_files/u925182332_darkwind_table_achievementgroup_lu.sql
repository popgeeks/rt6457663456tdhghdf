
-- --------------------------------------------------------

--
-- Table structure for table `achievementgroup_lu`
--

CREATE TABLE `achievementgroup_lu` (
  `AchievementGroupLU_ID` int(11) NOT NULL,
  `AchievementGroupLU_Description` varchar(256) NOT NULL,
  `AchievementGroupLU_ParentID` int(11) NOT NULL DEFAULT 20,
  `AchievementGroupLU_Depth` int(11) NOT NULL DEFAULT 0,
  `AchievementGroupLU_Lineage` varchar(500) NOT NULL DEFAULT '/'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci ROW_FORMAT=COMPACT;

--
-- Dumping data for table `achievementgroup_lu`
--

INSERT INTO `achievementgroup_lu` (`AchievementGroupLU_ID`, `AchievementGroupLU_Description`, `AchievementGroupLU_ParentID`, `AchievementGroupLU_Depth`, `AchievementGroupLU_Lineage`) VALUES
(1, 'Monthly Leaderboard', 22, 2, '/Competition/Monthly-Leaderboard/'),
(2, 'Membership', 3, 2, '/Community/Memberships/'),
(3, 'Community', 20, 1, '/Community/'),
(4, 'Quest', 9, 2, '/Feats-of-Strength/Quest/'),
(5, 'Cards', 20, 1, '/Cards/'),
(6, 'Statistical', 20, 1, '/Statistical/'),
(7, 'Chinchirorin', 21, 2, '/Game/Chinchirorin/'),
(8, 'Triple Triad War', 21, 2, '/Game/Triple-Triad-War/'),
(9, 'Feats of Strength', 20, 1, '/Feats-of-Strength/'),
(10, 'Triple Triad', 21, 2, '/Game/Triple-Triad/'),
(11, 'Omni Triple Triad', 21, 2, '/Game/Omni-Triple-Triad/'),
(12, 'Sphere Break', 21, 2, '/Game/Sphere-Break/'),
(13, 'Triple Triad Memory', 21, 2, '/Game/Triple-Triad-Memory/'),
(14, 'Team Omni Triple Triad', 21, 2, '/Game/Team-Omni-Triple-Triad/'),
(15, 'Ranking Leaderboard', 22, 2, '/Competition/Ranking-Leaderboard/'),
(16, 'Weekly Leaderboard', 22, 2, '/Competition/Weekly-Leaderboard/'),
(17, 'Development', 3, 2, '/Community/Development/'),
(18, 'Participation', 3, 2, '/Community/Participation/'),
(19, 'Administration', 9, 2, '/Feats-of-Strength/Administration/'),
(20, 'Uncategorized', 0, 0, '/'),
(21, 'Game', 20, 1, '/Game/'),
(22, 'Competition', 20, 1, '/Competition/'),
(23, 'Decks (2004)', 5, 2, '/Cards/Deck-2004/'),
(24, 'Currency', 6, 2, '/Statistical/Currency/'),
(25, 'Jackpots', 6, 2, '/Statistical/Jackpots/'),
(26, 'Wagers', 6, 2, '/Statistical/Wagers/'),
(27, 'Tournaments', 22, 2, '/Competition/Tournaments/'),
(28, 'Decks (2005)', 5, 2, '/Cards/Deck-2005/'),
(29, 'Decks (2006)', 5, 2, '/Cards/Deck-2006/'),
(30, 'Decks (2007)', 5, 2, '/Cards/Deck-2007/'),
(31, 'Decks (2008)', 5, 2, '/Cards/Deck-2008/'),
(32, 'Decks (2009)', 5, 2, '/Cards/Deck-2009/'),
(33, 'Decks (2010)', 5, 2, '/Cards/Deck-2010/'),
(34, 'Decks (2011)', 5, 2, '/Cards/Deck-2011/'),
(35, 'Decks (2012)', 5, 2, '/Cards/Deck-2012/');
