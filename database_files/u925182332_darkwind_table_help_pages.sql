
-- --------------------------------------------------------

--
-- Table structure for table `help_pages`
--

CREATE TABLE `help_pages` (
  `id` int(10) UNSIGNED NOT NULL,
  `topic` varchar(45) NOT NULL DEFAULT '' COMMENT 'Topic of the Help Item',
  `question` varchar(255) NOT NULL DEFAULT '' COMMENT 'The question header',
  `type` varchar(30) NOT NULL DEFAULT '' COMMENT 'The type of help (internet document, text, etc)',
  `description` text NOT NULL COMMENT 'The text of the help document or URL of the referal site'
) ENGINE=InnoDB DEFAULT CHARSET=latin1 COLLATE=latin1_swedish_ci;
