
-- --------------------------------------------------------

--
-- Table structure for table `info`
--

CREATE TABLE `info` (
  `id` int(10) UNSIGNED NOT NULL,
  `keyword` varchar(45) NOT NULL DEFAULT '',
  `info` varchar(2000) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `info`
--

INSERT INTO `info` (`id`, `keyword`, `info`) VALUES
(1, 'guildsetupcost', '5000'),
(2, 'guildsetuprules', 'Setting up a guild is easy.  Just enter a guild name that is less than 15 characters to start off.  If you setup a guild which is offensive, makes reference to illegal content, or violates the terms of service, the cost of the guild will be forfeited and will be disbanded. <BR><BR>The guild description will be what users see when they apply to your guild.');
