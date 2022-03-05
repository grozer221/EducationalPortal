import {gql} from '@apollo/client';

export const schema = gql`
    schema {
        query: Queries
        mutation: Mutations
    }

    type Queries {
        me: AuthResponseType!
        getEducationalYears(
            """
            Argument for get Educational years
            """
            page: Int! = 0
        ): GetEducationalYearResponseType!
        getEducationalYear(
            """
            Argument for get Educational year
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): EducationalYearType!
        getGrades(
            """
            Argument for get Grades
            """
            page: Int! = 0
        ): GetGradeResponseType!
        getGrade(
            """
            Argument for get Grade
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): GradeType!
        getSubject(
            """
            Argument for get Subject
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): SubjectType!
        getSubjects(
            """
            Argument for get Subjects
            """
            page: Int! = 0
        ): GetSubjectResponseType!
        getUsers(
            """
            Argument for get Users
            """
            page: Int! = 0

            """
            Argument for get Users
            """
            role: UserRoleEnum
        ): GetUserResponseType!
        getUser(
            """
            Argument for get User
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): UserType!
    }

    type AuthResponseType {
        user: UserType
        token: String
    }

    type UserType {
        id: ID!
        firstName: String
        lastName: String
        middleName: String
        login: String!
        email: String
        phoneNumber: String
        dateOfBirth: DateTime
        role: UserRoleEnum!
        createdAt: DateTime!
        updatedAt: DateTime!
        gradeId: ID
        grade: GradeType
        subjects(
            """
            Argument for get Subjects Posts
            """
            page: Int! = 0
        ): GetSubjectResponseType
    }

    """
    The \`DateTime\` scalar type represents a date and time. \`DateTime\` expects timestamps to be formatted in accordance with the [ISO-8601](https://en.wikipedia.org/wiki/ISO_8601) standard.
    """
    scalar DateTime

    enum UserRoleEnum {
        STUDENT
        TEACHER
        ADMINISTRATOR
    }

    type GradeType {
        id: ID!
        name: String!
        students(
            """
            Argument for get Subjects Posts
            """
            page: Int! = 0
        ): GetUserResponseType!
        createdAt: DateTime!
        updatedAt: DateTime!
    }

    type GetUserResponseType {
        entities: [UserType]!
        total: Int!
    }

    type GetSubjectResponseType {
        entities: [SubjectType]!
        total: Int!
    }

    type SubjectType {
        id: ID!
        name: String!
        link: String
        posts(
            """
            Argument for get Subjects Posts
            """
            page: Int! = 0
        ): GetSubjectPostResponseType
        teacherId: ID!
        teacher: UserType!
        educationalYearId: ID!
        educationalYear: EducationalYearType!
        createdAt: DateTime!
        updatedAt: DateTime!
    }

    type GetSubjectPostResponseType {
        entities: [SubjectPostType]!
        total: Int!
    }

    type SubjectPostType {
        id: ID!
        title: String!
        text: String
        type: PostType!
        teacherId: ID!
        teacher: UserType!
    }

    enum PostType {
        INFO
        HOMEWORK
    }

    type EducationalYearType {
        id: ID!
        name: String!
        dateStart: DateTime!
        dateEnd: DateTime!
        isCurrent: Boolean!
        createdAt: DateTime!
        updatedAt: DateTime!
    }

    type GetEducationalYearResponseType {
        entities: [EducationalYearType]!
        total: Int!
    }

    type GetGradeResponseType {
        entities: [GradeType]!
        total: Int!
    }

    type Mutations {
        login(
            """
            Argument for login User
            """
            loginAuthInputType: LoginAuthInputType!
        ): AuthResponseType!
        register(
            """
            Argument for register User
            """
            loginAuthInputType: LoginAuthInputType!
        ): AuthResponseType!
        createEducationalYear(
            """
            Argument for create new Educational year
            """
            createEducationalYearInputType: CreateEducationalYearInputType!
        ): EducationalYearType!
        updateEducationalYear(
            """
            Argument for update Educational year
            """
            updateEducationalYearInputType: UpdateEducationalYearInputType!
        ): EducationalYearType!
        removeEducationalYear(
            """
            Argument for remove Educational year
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): Boolean!
        createGrade(
            """
            Argument for create new Grade
            """
            createGradeInputType: CreateGradeInputType!
        ): GradeType!
        updateGrade(
            """
            Argument for update Grade
            """
            updateGradeInputType: UpdateGradeInputType!
        ): GradeType!
        removeGrade(
            """
            Argument for remove Grade
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): Boolean!
        createSubjectPost(
            """
            Argument for create new Subject Post
            """
            createSubjectPostInputType: CreateSubjectPostInputType!
        ): SubjectPostType!
        updateSubjectPost(
            """
            Argument for update Subject Post
            """
            updateSubjectPostInputType: UpdateSubjectPostInputType!
        ): SubjectPostType!
        removeSubjectPost(
            """
            Argument for remove Subject Post
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): Boolean!
        createSubject(
            """
            Argument for create new Subject
            """
            createSubjectInputType: CreateSubjectInputType!
        ): SubjectType!
        updateSubject(
            """
            Argument for update Subject
            """
            updateSubjectInputType: UpdateSubjectInputType!
        ): SubjectType!
        removeSubject(
            """
            Argument for remove Subject
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): Boolean!
        createUser(
            """
            Argument for create new User
            """
            createUserInputType: CreateUserInputType!
        ): UserType!
        updateUser(
            """
            Argument for update User
            """
            updateUserInputType: UpdateUserInputType!
        ): UserType!
        removeUser(
            """
            Argument for remove User
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): Boolean!
    }

    input LoginAuthInputType {
        login: String
        password: String
    }

    input CreateEducationalYearInputType {
        name: String
        dateStart: DateTime
        dateEnd: DateTime
    }

    input UpdateEducationalYearInputType {
        id: ID
        name: String
        isCurrent: Boolean
        dateStart: DateTime
        dateEnd: DateTime
    }

    input CreateGradeInputType {
        name: String!
    }

    input UpdateGradeInputType {
        id: ID!
        name: String!
    }

    input CreateSubjectPostInputType {
        title: String!
        text: String
        type: PostType!
        subjectId: ID!
    }

    input UpdateSubjectPostInputType {
        id: ID!
        title: String!
        text: String
        type: PostType!
    }

    input CreateSubjectInputType {
        name: String
    }

    input UpdateSubjectInputType {
        id: ID!
        name: String!
        link: String
    }

    input CreateUserInputType {
        firstName: String!
        lastName: String!
        middleName: String!
        login: String!
        password: String!
        email: String
        phoneNumber: String
        dateOfBirth: DateTime!
        role: UserRoleEnum!
        gradeId: ID
    }

    input UpdateUserInputType {
        firstName: String!
        lastName: String!
        middleName: String!
        login: String!
        password: String!
        email: String
        phoneNumber: String
        dateOfBirth: DateTime!
        role: UserRoleEnum!
        gradeId: ID
    }
`
