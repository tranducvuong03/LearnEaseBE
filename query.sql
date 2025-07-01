USE [LearnEase]

/*========================================================
  1. LANGUAGE & DIALECT
========================================================*/
INSERT INTO Languages(LanguageId, Name)
VALUES ('1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', N'English');

INSERT INTO Dialects
  (DialectId, LanguageId, Name, Region, AccentCode, Description, VoiceSampleUrl, IsAvailable)
VALUES
  ('3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01',
   N'General American', N'United States', 'en-US',
   N'Standard American English accent',
   'https://cdn.learnEase.com/audio/accent/en_us_sample.mp3', 1),
  ('3B5F8475-29D7-4F2D-A127-8CBFDF8D83F2', '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01',
   N'Received Pronunciation', N'United Kingdom', 'en-GB',
   N'Standard British English accent',
   'https://cdn.learnEase.com/audio/accent/en_gb_sample.mp3', 1);

/*========================================================
  2. USER & SETTING
========================================================*/
INSERT INTO [Users]
  (UserId, Username, Email, Password, AvatarUrl, CreatedAt)
VALUES
  ('4C6EC7A2-644C-44B8-B222-52EDF6197781', 'vuong', 'vuong@example.com', '123', NULL, SYSUTCDATETIME());

INSERT INTO UserSettings
  (UserId, PreferredDialectId, PlaybackSpeed, NotificationOn)
VALUES
  ('4C6EC7A2-644C-44B8-B222-52EDF6197781', '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1.0, 1);

/*========================================================
  3. VOCABULARY
========================================================*/
INSERT INTO VocabularyItems
  (VocabId, DialectId, Word, AudioUrl, ImageUrl, DistractorsJson)
VALUES
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'coffee', 'https://cdn.learnEase.com/audio/vocab/coffee_us.mp3', NULL, N'["toffee","coffer","copy"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'library', 'https://cdn.learnEase.com/audio/vocab/library_us.mp3', NULL, N'["librarian","liberty","battery"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'bicycle', 'https://cdn.learnEase.com/audio/vocab/bicycle_us.mp3', NULL, N'["tricycle","icicle","bisect"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'vegetable', 'https://cdn.learnEase.com/audio/vocab/vegetable_us.mp3', NULL, N'["vegetation","vestibule","vegetarian"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'airport', 'https://cdn.learnEase.com/audio/vocab/airport_us.mp3', NULL, N'["seaport","airplane","airdrop"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'mountain', 'https://cdn.learnEase.com/audio/vocab/mountain_us.mp3', NULL, N'["fountain","maintain","molecule"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'hospital', 'https://cdn.learnEase.com/audio/vocab/hospital_us.mp3', NULL, N'["hostile","hostel","postal"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'elephant', 'https://cdn.learnEase.com/audio/vocab/elephant_us.mp3', NULL, N'["relevant","elegant","eleven"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'umbrella', 'https://cdn.learnEase.com/audio/vocab/umbrella_us.mp3', NULL, N'["armadillo","umbrage","gazella"]'),
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', N'computer', 'https://cdn.learnEase.com/audio/vocab/computer_us.mp3', NULL, N'["commuter","conductor","composer"]');

/*========================================================
  4. SPEAKING EXERCISE (with fixed ID)
========================================================*/
INSERT INTO SpeakingExercises
  (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText)
VALUES
  ('5D7E5B9D-1144-4AD2-B719-179B1A93AC01',
   '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Please say: "How are you today?"',
   'https://cdn.learnEase.com/audio/exercise/how_are_you_today.mp3',
   'How are you today?');

/*========================================================
  5. USER PROGRESS & SPEAKING ATTEMPT
========================================================*/
INSERT INTO UserProgresses
  (ProgressId, UserId, VocabId, ExerciseId, LastReviewed, RepetitionCount)
VALUES
  ('9F2898C5-3B35-44F4-8AB3-0A0B0C0D0E01',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   NULL, NULL, DATEADD(DAY,-1,SYSUTCDATETIME()), 2),
  ('9F2898C5-3B35-44F4-8AB3-0A0B0C0D0E02',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   NULL, '5D7E5B9D-1144-4AD2-B719-179B1A93AC01', DATEADD(HOUR,-5,SYSUTCDATETIME()), 1);

INSERT INTO SpeakingAttempts
  (AttemptId, UserId, ExerciseId, AttemptedAt, Score, UserAudioUrl, Transcription)
VALUES
  ('AA1B2C3D-4E5F-6789-ABCD-EEFF00112233',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   '5D7E5B9D-1144-4AD2-B719-179B1A93AC01',
   SYSUTCDATETIME(), 85.0,
   'https://cdn.learnEase.com/audio/user/attempt_001.mp3',
   N'How are you today?');

/*========================================================
  6. ACHIEVEMENT & LEADERBOARD
========================================================*/
INSERT INTO Achievements
  (AchievementId, UserId, Name, Description, AchievedAt)
VALUES
  ('BBCCDD11-22EE-33FF-44AA-556677889900',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   N'First Lesson Completed',
   N'Hoàn thành bài học đầu tiên',
   DATEADD(HOUR,-2,SYSUTCDATETIME()));

INSERT INTO Leaderboards
  (LeaderboardId, UserId, Period, Score, RecordedAt)
VALUES
  ('CCDDEEFF-1122-3344-5566-77889900AABB',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',
   'weekly', 200,
   SYSUTCDATETIME());

/*========================================================
  7. SpeakingExercises
========================================================*/
INSERT INTO SpeakingExercises
  (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText)
VALUES
  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Say: "Where is the library?"',
   'https://www.oxfordlearnersdictionaries.com/media/english/us_pron/l/lib/library/library__us_1.mp3',
   'Where is the library?'),

  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Please say: "I need a computer."',
   'https://www.oxfordlearnersdictionaries.com/media/english/us_pron/c/com/computer/computer__us_1.mp3',
   'I need a computer.'),

  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Say aloud: "This is a hospital."',
   'https://www.oxfordlearnersdictionaries.com/media/english/us_pron/h/hos/hospit/hospital__us_1.mp3',
   'This is a hospital.'),

  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Repeat after me: "She rides a bicycle."',
   'https://www.oxfordlearnersdictionaries.com/media/english/us_pron/b/bic/bicycl/bicycle__us_1.mp3',
   'She rides a bicycle.'),

  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Please say: "I like vegetables."',
   'https://www.oxfordlearnersdictionaries.com/media/english/us_pron/v/veg/veget/vegetable__us_1.mp3',
   'I like vegetables.'),

  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Say this sentence: "The coffee is hot."',
   'https://www.oxfordlearnersdictionaries.com/media/english/us_pron/c/cof/coffee/coffee__us_1.mp3',
   'The coffee is hot.'),

  (NEWID(), '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Say: "It is raining heavily."',
   'https://www.oxfordlearnersdictionaries.com/media/english/us_pron/r/rai/rain_/rain__us_1.mp3',
   'It is raining heavily.');

   /*========================================================
  8. TOPIC & LESSON (Daily Life)
========================================================*/
INSERT INTO Topic(TopicId, Title, Description)
VALUES
  ('11111111-1111-1111-1111-111111111111', N'Daily Life', N'Từ vựng và câu nói hàng ngày');

INSERT INTO Lessons(LessonId, TopicId, DialectId, [Order], Title, Description)
VALUES
  ('22222222-2222-2222-2222-222222222222', '11111111-1111-1111-1111-111111111111',
   '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 1, N'Daily Vocabulary 1', NULL),

  ('22222222-2222-2222-2222-222222222223', '11111111-1111-1111-1111-111111111111',
   '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', 2, N'Daily Vocabulary 2', NULL);

/*========================================================
  9. GÁN 5 VOCAB CHO MỖI LESSON
========================================================*/
DECLARE @Lesson1 UNIQUEIDENTIFIER = '22222222-2222-2222-2222-222222222222';
DECLARE @Lesson2 UNIQUEIDENTIFIER = '22222222-2222-2222-2222-222222222223';

INSERT INTO LessonVocabularies (LessonId, VocabId)
SELECT @Lesson1, VocabId FROM VocabularyItems WHERE Word IN (
  N'coffee', N'library', N'bicycle', N'vegetable', N'airport'
);

INSERT INTO LessonVocabularies (LessonId, VocabId)
SELECT @Lesson2, VocabId FROM VocabularyItems WHERE Word IN (
  N'mountain', N'hospital', N'elephant', N'umbrella', N'computer'
);

/*========================================================
 10. GÁN 5 SPEAKING CHO MỖI LESSON
========================================================*/
INSERT INTO LessonSpeakings (LessonId, ExerciseId)
SELECT @Lesson1, ExerciseId FROM SpeakingExercises
WHERE ReferenceText IN (
  'How are you today?', 'Where is the library?', 'I need a computer.',
  'This is a hospital.', 'She rides a bicycle.'
);

INSERT INTO LessonSpeakings (LessonId, ExerciseId)
SELECT @Lesson2, ExerciseId FROM SpeakingExercises
WHERE ReferenceText IN (
  'I like vegetables.', 'The coffee is hot.', 'It is raining heavily.'
);
