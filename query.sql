USE [LearnEase]

/*========================================================
  1.  LANGUAGE & DIALECT
========================================================*/
INSERT INTO Languages(LanguageId, Name)
VALUES ('1A1D84D4-7E68-443D-8DFC-1E048C0ADF01', N'English');

INSERT INTO Dialects
  (DialectId, LanguageId, Name, Region, AccentCode, Description, VoiceSampleUrl, IsAvailable)
VALUES
  ('3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1', -- General American
   '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01',
   N'General American', N'United States', 'en-US',
   N'Standard American English accent',
   'https://cdn.learnEase.com/audio/accent/en_us_sample.mp3', 1),
  ('3B5F8475-29D7-4F2D-A127-8CBFDF8D83F2', -- Received Pronunciation
   '1A1D84D4-7E68-443D-8DFC-1E048C0ADF01',
   N'Received Pronunciation', N'United Kingdom', 'en-GB',
   N'Standard British English accent',
   'https://cdn.learnEase.com/audio/accent/en_gb_sample.mp3', 1);

/*========================================================
  2.  USER & SETTING
========================================================*/
INSERT INTO [Users]
  (UserId, Username, Email, Password, AvatarUrl, CreatedAt)
VALUES
  ('4C6EC7A2-644C-44B8-B222-52EDF6197781',  -- vuong
   'vuong', 'vuong@example.com', '123',
   NULL, SYSUTCDATETIME());

INSERT INTO UserSettings
  (UserId, PreferredDialectId, PlaybackSpeed, NotificationOn)
VALUES
  ('4C6EC7A2-644C-44B8-B222-52EDF6197781',  -- vuong
   '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',  -- Prefers American
   1.0, 1);

/*========================================================
  3.  VOCABULARY
========================================================*/
INSERT INTO VocabularyItems
  (VocabId, DialectId, Word, Meaning, AudioUrl, DistractorsJson)
VALUES
  ('8E0E90F8-9E1C-4B05-B6EC-001A1B2C3D01',
   '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'hello', N'xin chào',
   'https://cdn.learnEase.com/audio/vocab/hello_us.mp3',
   NULL),
  ('8E0E90F8-9E1C-4B05-B6EC-001A1B2C3D02',
   '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'schedule', N'lịch trình',
   'https://cdn.learnEase.com/audio/vocab/schedule_us.mp3',
   N'["shadow","school","schema"]');

/*========================================================
  4.  SPEAKING EXERCISE
========================================================*/
INSERT INTO SpeakingExercises
  (ExerciseId, DialectId, Prompt, SampleAudioUrl, ReferenceText)
VALUES
  ('5D7E5B9D-1144-4AD2-B719-179B1A93AC01',
   '3B5F8475-29D7-4F2D-A127-8CBFDF8D83F1',
   N'Please say: "How are you today?"',
   'https://cdn.learnEase.com/audio/exercise/how_are_you_today.mp3',
   N'How are you today?');

/*========================================================
  5.  USER PROGRESS & SPEAKING ATTEMPT
========================================================*/
INSERT INTO UserProgresses
  (ProgressId, UserId, VocabId, ExerciseId, LastReviewed, RepetitionCount)
VALUES
  ('9F2898C5-3B35-44F4-8AB3-0A0B0C0D0E01',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',   -- vuong
   '8E0E90F8-9E1C-4B05-B6EC-001A1B2C3D01',   -- hello
   NULL, DATEADD(DAY,-1,SYSUTCDATETIME()), 2),
  ('9F2898C5-3B35-44F4-8AB3-0A0B0C0D0E02',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',   -- vuong
   NULL,
   '5D7E5B9D-1144-4AD2-B719-179B1A93AC01',   -- exercise
   DATEADD(HOUR,-5,SYSUTCDATETIME()), 1);

INSERT INTO SpeakingAttempts
  (AttemptId, UserId, ExerciseId, AttemptedAt, Score, UserAudioUrl, Transcription)
VALUES
  ('AA1B2C3D-4E5F-6789-ABCD-EEFF00112233',
   '4C6EC7A2-644C-44B8-B222-52EDF6197781',   -- vuong
   '5D7E5B9D-1144-4AD2-B719-179B1A93AC01',
   SYSUTCDATETIME(), 85.0,
   'https://cdn.learnEase.com/audio/user/attempt_001.mp3',
   N'How are you today?');

/*========================================================
  6.  ACHIEVEMENT & LEADERBOARD
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
