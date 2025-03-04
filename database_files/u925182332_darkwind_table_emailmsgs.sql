
-- --------------------------------------------------------

--
-- Table structure for table `emailmsgs`
--

CREATE TABLE `emailmsgs` (
  `id` int(10) UNSIGNED NOT NULL,
  `keyword` varchar(45) NOT NULL DEFAULT '',
  `message` varchar(8000) NOT NULL DEFAULT ''
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `emailmsgs`
--

INSERT INTO `emailmsgs` (`id`, `keyword`, `message`) VALUES
(1, 'register', 'Welcome to Triple Triad Extreme!\\n\\nTriple Triad Extreme has been revived since March 13, 2005 after starting out in 2004.\\n\\nPlay triple triad with your friends or just socialize as triple triad extreme allows you to form guilds and compete on a ladder system.\\n\\nBelow is your new information.  Keep this email for your records! Have fun!\\n\\n'),
(2, 'infractionalert', 'ATTENTION Administrators!\r\n\r\nAt %%STAMP%%, a player infraction was applied to %%PLAYER%% by %%ADMIN%%:\r\n\r\nInfraction: %%INFRACTION%%\r\n\r\nReason: %%REASON%%\r\n');
