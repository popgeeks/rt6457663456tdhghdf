
-- --------------------------------------------------------

--
-- Table structure for table `tables`
--

CREATE TABLE `tables` (
  `ID` int(10) UNSIGNED NOT NULL,
  `TableID` int(10) UNSIGNED NOT NULL,
  `GameID` int(10) UNSIGNED NOT NULL,
  `Player1` varchar(45) NOT NULL,
  `Player2` varchar(45) NOT NULL,
  `Player3` varchar(45) NOT NULL,
  `Player4` varchar(45) NOT NULL,
  `Player_Ready1` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Player_Ready2` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Player_Ready3` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Player_Ready4` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `RuleList` varchar(200) NOT NULL,
  `Code_X` varchar(200) NOT NULL,
  `Comment` varchar(200) NOT NULL,
  `Lock` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Wager` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Stalemate1` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Stalemate2` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `Decks` varchar(1000) NOT NULL,
  `Blocks` varchar(5) NOT NULL,
  `GoldWager` int(10) UNSIGNED NOT NULL DEFAULT 0,
  `SkillRules` varchar(500) DEFAULT '',
  `TradeRules` varchar(200) DEFAULT '',
  `start_player_turn` int(1) DEFAULT 1,
  `max_lvl` int(2) DEFAULT 10,
  `min_lvl` int(2) DEFAULT 0,
  `wall_value` int(2) DEFAULT 10
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `tables`
--

INSERT INTO `tables` (`ID`, `TableID`, `GameID`, `Player1`, `Player2`, `Player3`, `Player4`, `Player_Ready1`, `Player_Ready2`, `Player_Ready3`, `Player_Ready4`, `RuleList`, `Code_X`, `Comment`, `Lock`, `Wager`, `Stalemate1`, `Stalemate2`, `Decks`, `Blocks`, `GoldWager`, `SkillRules`, `TradeRules`, `start_player_turn`, `max_lvl`, `min_lvl`, `wall_value`) VALUES
(26, 25, 2816940, 'p2k', 'klasse', 'ikinsey', '', 0, 0, 0, 0, '', '', 'Team Omni Triple Triad', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(27, 26, 2816844, 'GMK', 'daniell626', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(28, 27, 2816884, 'Sunshinelady', 'Masha013', '', '', 0, 0, 0, 0, '', '', 'OmniTripleTriad', 0, 0, 0, 0, 'Random', '5', 0, '', '', 1, 10, 0, 10),
(29, 28, 2816910, 'tukty', '', '', '', 0, 0, 0, 0, '', '', 'Chinchirorin', 0, 0, 0, 0, '', '', 10, '', '', 1, 10, 0, 10),
(30, 29, 2816917, 'Ico 1972', '', '', '', 0, 0, 0, 0, '', '', 'OmniTripleTriad', 0, 0, 0, 0, 'Player', '5', 0, '', '', 1, 10, 0, 10),
(31, 30, 2816924, 'uni_2751', '', '', '', 0, 0, 0, 0, '', '', 'OmniTripleTriad', 0, 0, 0, 0, 'Random', '5', 0, '', '', 1, 10, 0, 10),
(32, 31, 2816925, 'Kantian_dada', 'Gnikiv', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(33, 32, 2816933, 'pastaalpesto', 'radius75', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, 'KingdomHearts', '', 0, 'rank freeze', '', 1, 10, 0, 10),
(34, 33, 2816993, 'alberto', '', '', '', 0, 0, 0, 0, '', '', 'Triple Triad War', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(35, 34, 2816953, 'mgdc99', 'Zlaty', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(36, 35, 2816968, 'Sonny42', '', '', '', 0, 0, 0, 0, '', '', 'Sphere Break', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(37, 36, 2816964, 'Locarno', 'Nkaujsua', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, 'CC', '', 0, 'random', 'none', 1, 10, 0, 10),
(38, 37, 2816987, 'FynnSalabim', 'Matusa', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(39, 38, 2817014, 'Alyz', 'Sarisue', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(40, 39, 2817021, 'nazzyb', 'lucagian', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, '', '', 10, '', '', 1, 10, 0, 10),
(41, 40, 2817023, 'FuraFaolox', '', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, 'AllDecks', '', 0, 'random', 'none', 1, 10, 0, 10),
(42, 41, 2817036, 'joshuarothwe', 'birchof', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(43, 42, 2817037, 'Dso Bahamut', 'SlyArchy', '', '', 0, 0, 0, 0, '', '', 'TripleTriad', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(44, 43, 0, '', '', '', '', 0, 0, 0, 0, '', '', '', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(45, 44, 0, '', '', '', '', 0, 0, 0, 0, '', '', '', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(46, 45, 0, '', '', '', '', 0, 0, 0, 0, '', '', '', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(47, 46, 0, '', '', '', '', 0, 0, 0, 0, '', '', '', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(48, 47, 0, '', '', '', '', 0, 0, 0, 0, '', '', '', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(49, 48, 0, '', '', '', '', 0, 0, 0, 0, '', '', '', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(50, 49, 0, '', '', '', '', 0, 0, 0, 0, '', '', '', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10),
(51, 50, 0, '', '', '', '', 0, 0, 0, 0, '', '', '', 0, 0, 0, 0, '', '', 0, '', '', 1, 10, 0, 10);
