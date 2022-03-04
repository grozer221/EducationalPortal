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
        ): GetEducationalYearsResponseType!
        getEducationalYear(
            """
            Argument for get Educational year
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): EducationalYearType!
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
        ): GetSubjectsResponseType!
        getUsers(
            """
            Argument for get Users
            """
            page: Int! = 0
        ): GetUsersResponseType!
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

    type GetEducationalYearsResponseType {
        entities: [EducationalYearType]!
        total: Int!
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

    type SubjectType {
        id: ID!
        name: String!
        link: String
        posts(
            """
            Argument for get Subjects Posts
            """
            page: Int! = 0
        ): GetSubjectPostsResponseType
        teacherId: ID!
        teacher: UserType!
        educationalYearId: ID!
        educationalYear: EducationalYearType!
        createdAt: DateTime!
        updatedAt: DateTime!
    }

    type GetSubjectPostsResponseType {
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

    type GetSubjectsResponseType {
        entities: [SubjectType]!
        total: Int!
    }

    type GetUsersResponseType {
        entities: [UserType]!
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
        createSubjectPost(
            """
            Argument for create new Subject Post
            """
            createSubjectPostInputType: CreateSubjectPostInputType!
        ): SubjectPostType!
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

    input CreateSubjectPostInputType {
        title: String!
        text: String
        type: PostType!
        subjectId: ID!
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
        firstName: String
        lastName: String
        middleName: String
        login: String
        email: String
        isEmailConfirmed: Boolean
        phoneNumber: String
        dateOfBirth: DateTime
        role: UserRoleEnum
        gradeId: ID
    }
`
