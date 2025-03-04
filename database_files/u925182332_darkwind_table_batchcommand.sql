
-- --------------------------------------------------------

--
-- Table structure for table `batchcommand`
--

CREATE TABLE `batchcommand` (
  `BatchCommandID` int(10) UNSIGNED NOT NULL,
  `BatchCommandDescription` varchar(45) NOT NULL,
  `BatchCommandInterval` int(10) UNSIGNED NOT NULL,
  `BatchCommandSQL` varchar(300) NOT NULL,
  `BatchCommandRunLast` datetime NOT NULL DEFAULT '2000-01-01 00:00:00'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `batchcommand`
--

INSERT INTO `batchcommand` (`BatchCommandID`, `BatchCommandDescription`, `BatchCommandInterval`, `BatchCommandSQL`, `BatchCommandRunLast`) VALUES
(1, 'Player Silences', 1, 'call spu_PlayerSilence()', '2023-08-03 10:34:00'),
(2, 'Mass AP', 5, 'call usp_givemassap()', '2023-08-03 10:30:01'),
(5, 'Pandoras Box Reset', 5, 'call usp_PandorasBox()', '2023-08-03 10:30:01'),
(6, 'Leaderboard Reset and Ranking', 43200, 'call spt_Leaderboard()', '2099-06-30 23:57:49'),
(7, 'Completed Deck Achievements', 35, 'call spt_CompletedDeckAchievements()', '2023-08-03 10:25:01'),
(14, 'Statistical Achievements', 60, 'call spt_StatisticAchievements()', '2023-08-03 10:00:31'),
(17, 'Special Edition Card Achievements', 60, 'call spt_SpecialAchievementsList()', '2023-08-03 10:20:23'),
(19, 'Non-Special Deck Completition Achievements', 60, 'call spt_NonSpecialAchievementsList()', '2023-08-03 10:26:06'),
(20, 'Report: Online Users', 5, 'call spi_OnlineUsersReport()', '2023-08-03 10:33:00'),
(21, 'Portal Login-Logout Handler Job', 1, 'call spd_WebLogin', '2023-08-03 10:34:00'),
(22, 'Win Achievements', 60, 'call spt_WinAchievements()', '2023-08-03 10:27:09'),
(23, 'Report: Currency', 60, 'call spi_CurrencyReport()', '2023-08-03 10:27:11'),
(24, 'Level Achievements', 60, 'call spt_LevelAchievementsList()', '2023-08-03 09:45:33'),
(26, 'Online Activity', 1, 'call spi_OnlineActivity()', '2023-08-03 10:34:02'),
(27, 'Chinchirorin Wager Achievements', 60, 'call spt_ChinchirorinWagerAchievementList()', '2023-08-03 10:27:13'),
(28, 'Chinchirorin Total Wager Achievements', 60, 'call spt_ChinchirorinTotalWagerAchievementList()', '2023-08-03 10:27:13'),
(29, 'Triple Triad War: Combo Attack', 60, 'call spt_CardWars_ComboAttack()', '2023-08-03 07:49:42'),
(30, 'Triple Triad War: Critical Damage', 60, 'call spt_CardWars_CriticalDamage()', '2023-08-03 10:28:28'),
(31, 'Triple Triad War: Damage Achievements', 60, 'call spt_CardWars_DamageAchievements()', '2023-08-03 10:28:28'),
(32, 'Triple Triad War: Damage Blows', 60, 'call spt_CardWars_DamageBlow()', '2023-08-03 09:28:02'),
(33, 'Tables: Gold Wagers Achievements', 60, 'call spt_Tables_GoldWagers()', '2023-08-03 10:29:30'),
(34, 'Tables: Total Gold Wagers Achievements', 60, 'call spt_Tables_TotalGoldWagers()', '2023-08-03 10:29:31'),
(35, 'Triple Triad War: Strike Achievements', 60, 'call spt_CardWars_Strike()', '2023-08-03 10:30:03'),
(36, 'Triple Triad War: Elemental ATK Achievements', 60, 'call spt_CardWars_ElementalAttack()', '2023-08-02 13:08:01'),
(37, 'Membership Achievements', 60, 'call spt_MembershipAchievementsList()', '2023-08-03 08:32:43'),
(38, 'Active Point Decay', 43200, 'call spt_ActivePointDecay(6)', '2023-07-24 03:17:15'),
(42, 'Empty Guild Checker', 10, 'call spt_DeleteEmptyGuilds()', '2023-08-03 09:02:46'),
(43, 'Jackpot Achievements', 60, 'call spt_Achievements_Jackpots()', '2023-08-03 08:21:34'),
(44, 'Ladder Achievements', 60, 'call spt_LeaderboardAchievementsList()', '2023-08-03 09:32:31'),
(45, 'Forum AP Job', 10078, 'call spt_GiveForumAP()', '2023-08-02 22:12:04'),
(46, 'Forum Group Sync', 5, 'call tteforums.spt_ForumGroupSync()', '2018-09-20 07:02:20'),
(47, 'Forum Client Notifier', 5, 'call tteforums.spt_NotifyClient()', '2018-09-20 07:02:20'),
(48, 'Forum Post Client Notifier', 15, 'call tteforums.spt_NotifyClientofPosts()', '2018-06-18 20:41:11'),
(51, 'Daily Decay', 1440, 'call spt_DailyDecay()', '2023-08-03 03:09:23'),
(53, 'Generate Merit Score', 60, 'call spt_GenerateMeritScoreLeaderboard()', '2023-08-03 09:33:43'),
(54, 'Generate Card Leaderboard', 1440, 'call spt_GenerateCardLeaderboard()', '2023-08-02 11:31:52'),
(55, 'Process Auctions', 2, 'call spt_ProcessAuctions()', '2023-08-03 09:34:47'),
(56, 'Recent Scores', 5, 'call spt_RecentScores(\'TTW\')', '2023-08-03 09:35:19'),
(57, 'Recent Scores', 5, 'call spt_RecentScores(\'TTM\')', '2023-08-03 09:34:36'),
(58, 'Recent Scores', 5, 'call spt_RecentScores(\'OTT\')', '2023-08-03 09:36:43'),
(59, 'Recent Scores', 5, 'call spt_RecentScores(\'TOTT\')', '2023-08-03 09:37:47'),
(61, 'Recent Scores', 5, 'call spt_RecentScores(\'SB\')', '2023-08-03 09:38:56'),
(62, 'Recent Scores', 5, 'call spt_RecentScores(\'CHIN\')', '2023-08-03 09:39:05'),
(63, 'New Survivor', 60, 'call spt_NewSurvivor()', '2023-08-03 08:22:09'),
(64, 'Rush Decay', 1, 'call spd_Rush()', '2023-08-03 09:39:06');
