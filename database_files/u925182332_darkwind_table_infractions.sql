
-- --------------------------------------------------------

--
-- Table structure for table `infractions`
--

CREATE TABLE `infractions` (
  `ID` int(10) UNSIGNED NOT NULL,
  `InfractionDescription` varchar(250) NOT NULL,
  `InfractionPoints` int(10) UNSIGNED NOT NULL,
  `Silence` int(10) UNSIGNED NOT NULL,
  `SilenceMinutes` int(10) UNSIGNED NOT NULL,
  `RemoveByFine` int(10) UNSIGNED NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;

--
-- Dumping data for table `infractions`
--

INSERT INTO `infractions` (`ID`, `InfractionDescription`, `InfractionPoints`, `Silence`, `SilenceMinutes`, `RemoveByFine`) VALUES
(1, 'Filter Dodging', 1, 1, 5, 1),
(2, 'Flooding the Server', 1, 1, 10, 1),
(3, 'Using Third Party Tools to Exploit the Game', 100, 0, 0, 0),
(4, 'Swearing at a GM', 2, 1, 60, 0),
(5, 'Griefing Another Player (Minor)', 2, 1, 60, 0),
(6, 'Spamming Advertisement Links in any Chat', 5, 1, 60, 1),
(7, 'Paypal Chargeback After Services Rendered', 100, 0, 0, 0),
(8, 'Habitual Violation of Any Infraction', 5, 0, 0, 1),
(9, 'Multiple Accounts, Primary Account Penalty', 5, 0, 0, 0),
(10, 'Posting a Site Containing Illegal Material', 1, 1, 60, 1),
(11, 'Posting a Site Link Containing Pornographic Material', 1, 1, 120, 1),
(12, 'Posting Sexually Explicit Content in any Communication Channel', 2, 1, 60, 1),
(13, 'Avoiding a Temporary or Permanent Ban Through the Creation of a Multiple Account', 3, 0, 0, 0),
(14, 'Harassing or Griefing a GM', 3, 0, 0, 0),
(15, 'Exploiting a Bug or Glitch to Gain an Advantage in Game Play', 5, 0, 0, 0),
(16, 'Excessive Quitting of Trade Games', 2, 0, 0, 1),
(17, 'An Emergency Ban Was Applied', 3, 0, 0, 0),
(18, 'Sending Offensive Content in Moogle Mail', 2, 0, 0, 1),
(19, 'Abusing the Report Moogle Mail as Offensive/Spam Function', 1, 0, 0, 1),
(20, 'Abusing the Card Lore Submission Function', 1, 0, 0, 1),
(21, 'Excessive Whining and Annoying Main Chat', 1, 1, 60, 1),
(22, 'Theft of an In-Game Currency Object that has Monetary Value, Minor', 5, 0, 0, 1),
(23, 'Theft of an In-Game Currency Object that has Monetary Value, Major - Prosecution Possible', 10, 0, 0, 0),
(24, 'Exploiting a Ruleset to Gain an Advantage in the Game (Turboing)', 3, 0, 0, 0),
(25, 'Using a surname that contains foul language/filtered language in any language', 5, 0, 0, 1),
(26, 'Player name contains foul language/filtered language in any language (instant ban)', 10, 0, 0, 0),
(27, 'Abusing the Malform Filter', 2, 0, 0, 1),
(29, 'Losing Games on Purpose to Raise Rank for Another Player to Win the Leaderboard', 2, 0, 0, 0),
(30, 'Being the Recipient of Free Rank to Win the Leaderboard by Manipulation', 2, 0, 0, 0),
(31, 'Griefing Another Player (Major)', 4, 0, 60, 0);
