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
        ): [EducationalYearType]!
        getEducationalYear(
            """
            Argument for set current Educational year
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): EducationalYearType!
        getSubjects(
            """
            Argument for get Subjects
            """
            page: Int! = 0
        ): [SubjectType]!
        getSubject(
            """
            Argument for get Subject
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): SubjectType!
        getUsers(
            """
            Argument for get Users
            """
            page: Int! = 0
        ): [UserType]!
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
        id: ID
        firstName: String
        lastName: String
        middleName: String
        login: String
        email: String
        phoneNumber: String
        dateOfBirth: DateTime
        role: UserRoleEnum
        createdAt: DateTime
        updatedAt: DateTime
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

    type EducationalYearType {
        id: ID
        name: String
        dateStart: DateTime
        dateEnd: DateTime
        isCurrent: Boolean
        createdAt: DateTime
        updatedAt: DateTime
    }

    type SubjectType {
        id: ID
        name: String
        link: String
        teacherId: ID!
        teacher: UserType!
        educationalYearId: ID!
        educationalYear: EducationalYearType!
        createdAt: DateTime!
        updatedAt: DateTime!
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
        setCurrentEducationalYear(
            """
            Argument for set current Educational year
            """
            id: ID! = "00000000-0000-0000-0000-000000000000"
        ): EducationalYearType!
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
        email: String
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
        dateStart: DateTime
        dateEnd: DateTime
    }

    input CreateSubjectInputType {
        name: String
    }

    input UpdateSubjectInputType {
        id: ID!
        name: String!
        link: String!
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
